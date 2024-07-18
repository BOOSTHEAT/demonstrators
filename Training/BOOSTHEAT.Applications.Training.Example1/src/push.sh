
usage() {
  echo "Usage: $0 <target-ip-address>"
  exit 1
}

if [ $# != 1 ]; then usage; fi
SCRIPT_FOLDER=$(dirname $0)
ROOT=$(readlink -f $SCRIPT_FOLDER/../..)
"$SCRIPT_FOLDER/../../push_training.sh" "Example1" $1
