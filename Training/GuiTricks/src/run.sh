APP_CSPROJ=Training.GuiTricks.csproj
APP_MAIN=Training.GuiTricks.Main
APP_OUTPUT=/tmp/Training.GuiTricks

dotnet restore --interactive "${APP_CSPROJ}"
dotnet build "${APP_CSPROJ}"
rm -rf "${APP_OUTPUT}.zip" "${APP_OUTPUT}"

$IMPLICIX_LINKER/ImpliciX.Linker build \
  -s https://pkgs.dev.azure.com/boostheat/_packaging/ImpliciX/nuget/v3/index.json \
  -n ImpliciX.Runtime \
  -t linux-x64 \
  -p "${APP_CSPROJ}" \
  -e "${APP_MAIN}" \
  -v "8.8.8.888" \
  -o "${APP_OUTPUT}.zip"

unzip "${APP_OUTPUT}.zip" -d "${APP_OUTPUT}"

export IMPLICIX_ENVIRONMENT=dev
export IMPLICIX_LOCAL_STORAGE=/tmp/slot
"${APP_OUTPUT}/BOOSTHEAT.Boiler.App"

rm -rf "${APP_OUTPUT}.zip" "${APP_OUTPUT}"
