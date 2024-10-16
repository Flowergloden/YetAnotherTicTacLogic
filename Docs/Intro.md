﻿# 项目讲解

这部分内容主要针对项目的整体设计进行讲解

## 项目结构

项目主体使用 Unity 进行开发, 
除了 Unity 项目之外, 还额外做了一些 Trick, 使得 Nuget 包可以自动直接打进 Unity 项目中, 
另外本项目包含两个 Git Submodule, 是个人实现的两个搜索算法类库

## 底层设计

项目使用类似于 MVC 的设计模式, 底层类分为逻辑层、表现层、控制层三部分,
其中逻辑层仅持有数据本身, 而表现层从逻辑层拉取数据并做视觉表现上的更新, 控制层则提供各种使用或修改逻辑层数据的操作，
详见开发者文档

## AI

项目中在不同的难度中分别使用了 Minimax 和 Monte Carlo Tree Search 两套搜索算法, 
理论上 Monte Carlo Tree Search 应该在大棋盘上表现更好, 
但实际上由于算法的效率及算力的限制, 实际模拟次数没上去的情况下与最优解相差甚远,
真要继续迭代还得上神经网络

## UI

本人并不擅长使用支持 Unity 各种成熟 UI 解决方案, 因此项目中临时使用 IMGUI 方案, 仅实现功能, 并没有过多考虑操作逻辑及美观

## 配置

项目中使用 ScriptableObject 储存游戏相关的配置项, 并提供 Excel 读取方案, 详见策划文档