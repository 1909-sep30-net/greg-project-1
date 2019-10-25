FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build

WORKDIR /app

COPY *.sln ./
COPY Data.Library/*.csproj Data.Library/
COPY Domains.Library/*.csproj Domains.Library/
COPY Greg-Project-1/*.csproj Greg-Project-1/
COPY XUnitDomainsLibrary/*.csproj XUnitDomainsLibrary/

RUN dotnet restore

# if the csproj files have not changed since the last build
# on this laptop, then, the above layers will be recovered from cache,
# and we don't need to run restore again.

# we use .dockerignore to hide files from being copied with
# the build context, so COPY here won't see them either.
# which files? bin/, obj/, etc.
COPY . ./

RUN dotnet publish . -c Release -o publish --no-restore

# to avoid keeping the SDK around in the image we want to distribute
# our app, we use multi-stage build.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS http://+:80

CMD [ "dotnet", "Greg-Project-1.dll" ]
# this final image does not have the SDK, nor the source code,
# only 1. what's in the base image, #2 my published build output.
