FROM mono:onbuild
EXPOSE 8080
CMD ["mono", "./Api.exe"]