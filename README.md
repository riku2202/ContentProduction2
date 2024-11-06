# コンテンツ制作デジタル2回目
◇ドライブURL

https://drive.google.com/drive/folders/1OkbKSvit6L9NQAdWteW1DFBfG64sd-mM?lfhs=2

◇スケジュール表URL

https://docs.google.com/spreadsheets/d/1bBX-Ei3bcRQHsn_kJCe9un3w8dvhmYTq4viBJb9KGwQ/edit?gid=0#gid=0

◇プロジェクトの入れ方

git clone https://**github.com/riku2202/ContentProduction2.git**

◇ブランチの操作

・ブランチの作成　　　　
git branch ブランチ名

・ブランチの切り替え　　
git checkout ブランチ名

・ブランチの削除　　　　
git branch -d ブランチ名

◇プロジェクトを最新の状態にする

git fetch origin

git merge origin/ブランチ名

◇プロジェクトの更新をリモートにアップする

git add .

git commit -m "作業内容"

git push origin ブランチ名

※一度だけgit push -u origin ブランチ名を実行すると
いつも通りgit pushのみで大丈夫です
