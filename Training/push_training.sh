#!/bin/bash

usage() {
  echo "Usage: $0 <training-id> <target-ip-address>"
  exit 1
}

if [ $# != 2 ]; then usage; fi

TRAINING_ID="$1"
IP="$2"
SCRIPT_FOLDER=$(dirname "$0")
ROOT=$(readlink -f "$SCRIPT_FOLDER/../..")
APPLICATION=$(echo "$ROOT"/Applications/Training/BOOSTHEAT.Applications.Training.${TRAINING_ID}/src/*.csproj)
ENTRY_POINT="BOOSTHEAT.Applications.Training.${TRAINING_ID}.Main"
${ROOT}/Tools/Deployment/push_update_package.sh $IP "$TRAINING_ID" "$TRAINING_ID" "$APPLICATION" "$ENTRY_POINT"
