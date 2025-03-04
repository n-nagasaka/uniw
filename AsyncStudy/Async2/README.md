# Async2

.NET async の動作調査

`BackgroundService` アプリケーションで `async void` のメンバーは実行できるが
処理内で例外が発生した場合にアプリケーションの実行が停止する


AsyncVoidExec 内で例外が発生するとアプリケーションがクラッシュする
```
> dotnet run void error
```

パラメータ  
void : async void メンバーを実行  
error: それぞれの処理内で Exception を発生させる  



```
> dotnet run error
```
`Exec`，`ExecWithoutErrorHandling` ともに `async Task` であり
例外が発生してもアプリケーションは実行を続ける
