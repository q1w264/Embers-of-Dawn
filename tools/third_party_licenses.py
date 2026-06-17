#!/usr/bin/env python3
from __future__ import annotations

import argparse
import json
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
LOCK_FILE = ROOT / "Packages" / "packages-lock.json"
MANUAL_FILE = ROOT / "third-party-licenses" / "manual-entries.json"
OUTPUT_FILE = ROOT / "THIRD_PARTY_LICENSES.md"

UNITY_TERMS_URL = "https://unity.com/legal/terms-of-service/software"


def _read_json(path: Path):
    with path.open("r", encoding="utf-8") as handle:
        return json.load(handle)


def load_unity_dependencies():
    lock = _read_json(LOCK_FILE)
    dependencies = lock.get("dependencies", {})
    rows = []
    for name, data in dependencies.items():
        version = data.get("version", "unknown")
        depth = data.get("depth", 0)
        source_type = data.get("source", "unknown")
        registry_url = data.get("url", "https://packages.unity.com")
        source_url = registry_url if source_type == "registry" else "https://docs.unity3d.com"
        rows.append(
            {
                "name": name,
                "version": version,
                "scope": "direct" if depth == 0 else "indirect",
                "source_url": source_url,
                "license_type": "Unity package terms",
                "spdx": "NOASSERTION",
                "copyright": "NOASSERTION",
                "license_text_link": UNITY_TERMS_URL,
            }
        )
    return sorted(rows, key=lambda item: item["name"].lower())


def load_manual_entries():
    entries = _read_json(MANUAL_FILE)
    normalized = []
    for entry in entries:
        for asset_path in entry.get("asset_paths", []):
            if not (ROOT / asset_path).exists():
                raise FileNotFoundError(f"Missing declared asset path: {asset_path}")
        license_text_path = entry.get("license_text_path")
        if license_text_path and not (ROOT / license_text_path).exists():
            raise FileNotFoundError(f"Missing declared license text path: {license_text_path}")
        normalized.append(
            {
                "name": entry["name"],
                "version": entry["version"],
                "category": entry["category"],
                "asset_paths": entry.get("asset_paths", []),
                "source_url": entry["source_url"],
                "license_type": entry["license_type"],
                "spdx": entry["spdx"],
                "copyright": entry["copyright"],
                "license_text_link": entry["license_text_link"],
                "license_text_path": license_text_path,
            }
        )
    return sorted(normalized, key=lambda item: item["name"].lower())


def _md_link(text: str, url: str) -> str:
    if not url or url == "NOASSERTION":
        return text
    return f"[{text}]({url})"


def _table(headers: list[str], rows: list[list[str]]) -> str:
    out = [
        "| " + " | ".join(headers) + " |",
        "| " + " | ".join(["---"] * len(headers)) + " |",
    ]
    out.extend("| " + " | ".join(row) + " |" for row in rows)
    return "\n".join(out)


def render_document() -> str:
    unity = load_unity_dependencies()
    manual = load_manual_entries()

    unity_rows = [
        [
            item["name"],
            item["version"],
            item["scope"],
            _md_link("link", item["source_url"]),
            item["license_type"],
            item["spdx"],
            item["copyright"],
            _md_link("link", item["license_text_link"]),
        ]
        for item in unity
    ]

    manual_rows = [
        [
            item["name"],
            item["version"],
            item["category"],
            ", ".join(item["asset_paths"]) if item["asset_paths"] else "NOASSERTION",
            _md_link("link", item["source_url"]),
            item["license_type"],
            item["spdx"],
            item["copyright"],
            _md_link("link", item["license_text_link"]),
            item["license_text_path"] or "NOASSERTION",
        ]
        for item in manual
    ]

    include_text_spdx = {"MIT", "Apache-2.0", "BSD-2-Clause", "BSD-3-Clause", "OFL-1.1"}
    include_text_rows = [
        f"- {item['name']} ({item['spdx']}): `{item['license_text_path']}`"
        for item in manual
        if item["spdx"] in include_text_spdx and item.get("license_text_path")
    ]
    if not include_text_rows:
        include_text_rows = ["- No dependency in the current inventory requires bundled license text from this rule set."]

    copyleft_rows = []
    for item in unity + manual:
        spdx = item["spdx"].upper()
        if "GPL" in spdx or "LGPL" in spdx or "MPL" in spdx:
            copyleft_rows.append(
                f"- {item['name']} ({item['spdx']}): provide source/modification notices and satisfy corresponding distribution conditions."
            )
    if not copyleft_rows:
        copyleft_rows = ["- No GPL/LGPL/MPL dependencies detected in current inventory."]

    sections = [
        "# THIRD_PARTY_LICENSES",
        "",
        "This file is generated by `tools/third_party_licenses.py` and consolidates direct/indirect package dependencies plus non-package third-party assets/fonts used by the project.",
        "",
        "## 1) Unity package dependencies (direct + indirect)",
        "",
        _table(
            ["Name", "Version", "Scope", "Source", "License Type", "SPDX", "Copyright", "License Text"],
            unity_rows,
        ),
        "",
        "## 2) Non-package third-party assets/fonts",
        "",
        _table(
            [
                "Name",
                "Version",
                "Category",
                "Used In",
                "Source",
                "License Type",
                "SPDX",
                "Copyright",
                "License Text",
                "Bundled License Path",
            ],
            manual_rows,
        ),
        "",
        "## 3) Dependencies requiring bundled license text",
        "",
        "\n".join(include_text_rows),
        "",
        "## 4) Copyleft distribution obligations (GPL/LGPL/MPL)",
        "",
        "\n".join(copyleft_rows),
        "",
        "## 5) Release check",
        "",
        "- Run `python3 tools/third_party_licenses.py --check` before release.",
        "- If check fails, run `python3 tools/third_party_licenses.py --generate`, review diffs, and commit updated license inventory.",
    ]
    return "\n".join(sections) + "\n"


def generate() -> int:
    OUTPUT_FILE.write_text(render_document(), encoding="utf-8")
    print(f"Generated: {OUTPUT_FILE}")
    return 0


def check() -> int:
    expected = render_document()
    current = OUTPUT_FILE.read_text(encoding="utf-8") if OUTPUT_FILE.exists() else ""
    if expected != current:
        print("THIRD_PARTY_LICENSES.md is outdated. Run: python3 tools/third_party_licenses.py --generate")
        return 1
    print("THIRD_PARTY_LICENSES.md is up to date.")
    return 0


def main() -> int:
    parser = argparse.ArgumentParser(description="Generate/check third-party licenses inventory")
    mode = parser.add_mutually_exclusive_group(required=True)
    mode.add_argument("--generate", action="store_true", help="Generate THIRD_PARTY_LICENSES.md")
    mode.add_argument("--check", action="store_true", help="Check THIRD_PARTY_LICENSES.md matches generated output")
    args = parser.parse_args()

    if args.generate:
        return generate()
    return check()


if __name__ == "__main__":
    raise SystemExit(main())
