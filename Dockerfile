# 使用一个基础镜像作为起点
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# 设置工作目录
WORKDIR /app

# 复制项目文件到容器内
COPY . ./

# 运行 restore 命令恢复依赖项
RUN dotnet restore

# 运行 build 命令构建项目
RUN dotnet build -c Release -o out

# 设置运行时镜像
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# 设置工作目录
WORKDIR /app

# 复制构建产物到容器内
COPY --from=build-env /app/out .

# 声明容器要监听的端口
EXPOSE 8880

# 定义运行时命令
ENTRYPOINT ["dotnet", "QuotationServiceManagement.Application.Web.dll"]
