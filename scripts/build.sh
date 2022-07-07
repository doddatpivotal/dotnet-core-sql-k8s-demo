pushd employee-todo-list-app
npm install
npm run build
popd
rm -rf employee-todo-list-api/wwwroot
cp -R employee-todo-list-app/dist employee-todo-list-api/wwwroot
dotnet build employee-todo-list-api