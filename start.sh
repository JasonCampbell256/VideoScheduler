#!/bin/bash
# Ensure we are running from the directory where the script is located
cd "$(dirname "$0")"

# Run the application, passing any arguments along
./VideoScheduler "$@"
