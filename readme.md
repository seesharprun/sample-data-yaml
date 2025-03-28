# YAML sample data

This repository illustrates how to share sample data and auto-build release assets.

The sample data in this repository is in the **YAML** format and is auto-built to **JSON**, **XML**, and **CSV** for any platform.

## Features

- Stores sample data in YAML files in the `/data` folder
- Has a CLI tool in the `/src` folder that generates data in other formats
- Uses a GitHub workflow to automate most release actions
  - Runs the tool multiple times to generate the data in various formats:
    - **JSON**
    - **XML**
    - **CSV**
  - Creates a GitHub release with the script file and backup file
