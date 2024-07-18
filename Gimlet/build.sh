#!/bin/bash

[[ $@ == *"NOAPP"* ]] || manifestIdList+=" -a device:app"
[[ $@ == *"NOGUI"* ]] || manifestIdList+=" -g device:gui"

"$IMPLICIX_LINKER/buildAppPackage.sh" -p "./Gimlet.App/src/Gimlet.App.csproj" -n "Gimlet" -e "Gimlet.App.Main" $manifestIdList -o "/tmp/gimlet"
