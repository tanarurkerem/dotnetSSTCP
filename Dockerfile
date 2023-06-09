FROM mcr.microsoft.com/dotnet/sdk:6.0
RUN groupadd --gid 1000 dotnet \
  && useradd --uid 1000 --gid dotnet --shell /bin/bash --create-home dotnet