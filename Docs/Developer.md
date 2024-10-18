# 开发人员文档

这部分主要介绍各模块的功能及拓展

## 逻辑层

LogicLayer 是整个逻辑层的数据表示，目前仅包含整个棋盘的数据，数据仅包含棋子类型\
如果希望逻辑层携带更多数据, 可以拓展该类或修改 MapData 类

## 表现层

InterfaceLayer 是一个 MonoBehaviour, 负责侦测逻辑层的变化并做出相应更新\
如果希望更改表现层的行为, 可以修改 Update() 中的对应操作

## 控制层

控制层包含针对逻辑层的若干读取及修改操作, 并提供一定 API

### 逻辑层更新

LogicLayerUpdater 是一个纯方法类, 提供了针对逻辑层的两个修改操作, 分别为初始化和更新特定位置

### 棋盘检测

WinnerDetector 是一个 MonoBehaviour, 最早用于检测是否已经产生了胜者,
后来为了接入后面要提到的搜索库, 提取了方法并开放 API, 现在可以检测任意行列或对角线是否有 n 个连续棋子

### UI 绘制

PlacementButtonDrawer 负责在棋盘上的空位对应的屏幕空间绘制 UI 用于落子, 如果更改了 UI 方案, 则需要同步修改该类

### Minimax / MCTS 交互接口

MCTSInterface 实现了 Minimax 和 MCTS 要求实现的 Game 类, 详见对应项目的文档

## GameManager

GameManager 是一个默认放置在场景中的 MonoBehaviour, 用于控制整个游戏进程,
大部分 API 在实现后需要在这里进行统一调用以避免时序问题