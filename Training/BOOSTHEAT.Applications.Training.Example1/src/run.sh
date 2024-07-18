APP_CSPROJ=BOOSTHEAT.Applications.Training.Example1.csproj
APP_MAIN=BOOSTHEAT.Applications.Training.Example1.Main
APP_OUTPUT=/tmp/Example1

dotnet restore --interactive "${APP_CSPROJ}"
dotnet build "${APP_CSPROJ}"

$IMPLICIX_LINKER/ImpliciX.Linker build-from-source \
  -s https://pkgs.dev.azure.com/boostheat/_packaging/ImpliciX/nuget/v3/index.json \
  -r ImpliciX.Runtime \
  -t linux-x64 \
  -a "${APP_CSPROJ}" \
  -e "${APP_MAIN}" \
  -v "8.8.8.888" \
  -o "${APP_OUTPUT}.zip"

unzip "${APP_OUTPUT}.zip" -d "${APP_OUTPUT}"

export IMPLICIX_ENVIRONMENT=dev
export IMPLICIX_LOCAL_STORAGE=/tmp/slot
"${APP_OUTPUT}/BOOSTHEAT.Boiler.App"

rm -rf "${APP_OUTPUT}.zip"
