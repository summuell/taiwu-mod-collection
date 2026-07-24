# 自用合集

《太吾绘卷：天幕心帷》个人 Mod 合集。

## 功能

### 云游道易天改命无配平
- 云游道易天改命 NPC 特性的交换不再检查双方交换数量的平衡性
- 解除交换按钮的默认禁用状态（血量过低时仍保持禁用）

### 出生特质无限制
- 出生特质选择界面移除所有特质的前置点数与消耗点数限制
- 所有特质免费、无前置条件可选

## 安装

将 `mod/` 目录下的 `自用合集` 文件夹复制到游戏 Mod 目录：

```
<游戏根目录>/Mod/自用合集/
```

在游戏内 Mod 管理器中启用即可。

## 构建

需要 .NET 8+ SDK。前端项目目标 `netstandard2.1`，后端项目目标 `net8.0`。

```powershell
# 前端
dotnet build src/EasyFeatureExchange/EasyFeatureExchange.csproj -c Release

# 后端
dotnet build src/EasyFeatureExchange.Backend/EasyFeatureExchange.Backend.csproj -c Release
```

产物输出到 `mod/Plugins/`。
