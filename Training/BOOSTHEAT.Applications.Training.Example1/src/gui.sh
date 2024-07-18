#!/bin/bash
set -e

usage() {
  echo "$0 (VNC|X11) [<backend IP>]"
  echo "VNC: run GUI as a remote VNC server"
  echo "XCB: run GUI by connecting to the local X11 display"
  exit 1
}
if [ $# -lt 1 ]; then usage > /dev/stderr; fi

RUN_GUI="${IMPLICIX_LINKER}/runGUI.sh"

config_error() {
  echo "Cannot find ${RUN_GUI}."
  echo "IMPLICIX_LINKER should contain the ImpliciX linker install folder."
  exit 1
}

if [ ! -f ${RUN_GUI} ]
then
  config_error > /dev/stderr
fi

SCRIPT_FOLDER=$(dirname $(readlink -f $0))
${RUN_GUI} ${SCRIPT_FOLDER}/BOOSTHEAT.Applications.Training.Example1.csproj BOOSTHEAT.Applications.Training.Example1.Main $1 $2
