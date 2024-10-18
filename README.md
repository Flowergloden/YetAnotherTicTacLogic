# Yet Another Tic-Tac-Logic

## Introduction

一个井字棋小游戏

## 项目部署

Unity 引擎版本为 2022.3.49f1\
理论上直接克隆仓库即可

### 可能遇到的问题

- Submodule 丢失:\
  手动进行 git Submodule update
  或在 idea 版本控制中将两个 Submodule 添加为独立项目
- Assets/Plugins/** 动态链接库报错:\
  可能是由于 meta 文件丢失, 或是平台设置不一致, 需要手动删除报错的动态链接库, 或是在引擎中找到报错的动态链接库, 反选
  Include Platforms 中的所有平台

## 游戏游玩方式

打开可运行二进制文件, 选择 Difficulty, 在游戏期间任意时刻点击开始游戏即可开始新的一局, 当任意一方获胜后, 游戏流程会终止,
点击弹出的重新开始即可

## 文档

[项目介绍](Docs/Intro.md)\
[开发者文档](Docs/Developer.md)\
[策划文档](Docs/Designer.md)

## 相关链接

[Minimax个人实现](https://github.com/Flowergloden/MinimaxCS)\
[MonteCarloTreeSearch个人实现](https://github.com/Flowergloden/MCTSCS)

## 开源许可证说明

[EPPlus](https://polyformproject.org/licenses/noncommercial/1.0.0/)