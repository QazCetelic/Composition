#!/bin/sh

install_dir="$HOME/.local/share"
# Move binary to install directory
mkdir -p "$install_dir/composition"
# Binary located at bin/Release/net6.0/linux-x64/publish
release_binary="$(pwd)/bin/Release/net6.0/linux-x64/publish/Composition"
# Desktop file composition.desktop
desktop_file="$(pwd)/composition.desktop"
# Verify that the desktop file exists
if [ ! -f "$desktop_file" ]; then
    echo "Desktop file not found $desktop_file ensure that you are in the directory the script is located in"
    exit 1
fi
# Verify that the binary exists
if [ ! -f "$release_binary" ]; then
    echo "Binary not found at $release_binary, run 'dotnet publish -c Release --self-contained -p:PublishSingleFile=true,PublishTrimmed=true' first"
    exit 1
fi
# Copy binary to install directory
cp "$release_binary" "$install_dir/composition"
# Copy desktop file to ~/.local/share/applications/
cp "$desktop_file" "$HOME/.local/share/applications/"