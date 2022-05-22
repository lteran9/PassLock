# PassLock
.NET Core console application that will manage, encrypt, serialize, and decrypt passwords to a JSON file.

## Publish Executable
dotnet publish \
    --configuration Release \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true \
    -p:CopyOutputSymbolsToPublishDirectory=false \
    --self-contained \