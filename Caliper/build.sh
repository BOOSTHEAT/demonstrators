#!/bin/bash

[[ $@ == *"NOAPP"* ]] || manifestIdList+=" -a device:app"
[[ $@ == *"NOGUI"* ]] || manifestIdList+=" -g device:gui"

"$IMPLICIX_LINKER/buildAppPackage.sh" -p "./Caliper.App/src/Caliper.App.csproj" -n "Caliper" -e "Caliper.App.Main" $manifestIdList -o "/tmp/caliper"
