# unity-reflection-samples

Unity向け、C#のReflectionを使ってComponentの各種値をDictionary<任意のキー, object>に格納・取出するサンプルコードです。

* pros
  * プログラムの再コンパイル・変更なしでComponent間の値を取り回しできる。
  * AssetBundle等から利用すればComponent
* cons
  * Reflectionは直接の値操作に比べてとても遅い。
  * サンプルコードはチューニングを一切行っていないので最適化の余地が多々ある。

## 使いかた

* Sceneに1つBlackboard-Componentを置く。
* 任意のComponentにPropertyToBlackboard-componentを置く。
  * Inspectorからコピー元を選択する。（Componnet名を選択、Property名を選択）
  * Inspectorからコピー先のBlackboardでの変数名を設定する。
* 任意のComponentにBlackboardToProperty-componentを置く。
  * Inspectorからコピー元のBlackboardでの変数名を設定する。
  * Inspectorからコピー先を選択する。（Componnet名を選択、Property名を選択）

実行してBlackboardをInspectorで確認するとPropertyToBlackboardで設定した値が入ってくる。
（なお値は毎フレーム更新されるが、Inspector上の表示はリアルタイム更新はされない）
またBlackboardToPropertyで設定した値が毎フレーム更新される。
