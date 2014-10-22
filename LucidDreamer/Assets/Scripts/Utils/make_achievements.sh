#!/bin/bash

num_achievements=`cat achievements.txt | grep ^achievement | wc -l`

echo "/* This file is generated by make_achievements.sh */
using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public  class AchievementsList {

// Yes the default value is false, http://msdn.microsoft.com/en-us/library/c8f5xwh7.aspx
private bool[] executed = new bool[$num_achievements];
" > AchievementsList.cs

i=0
while read item; do
  name=`echo $item | cut -f1 -d","`
  key=`echo $item | cut -f2 -d","`
  if [[ $name == achievement* ]]; then
	echo "	public void Get${name:11}() {
		if (!executed[$i]) {
			try {
				Social.ReportProgress ($key, 100.0f, (bool success) => {
					// handle success or failure
				});
				executed[$i] = true;
			} catch {}
		}
	}
	" >> AchievementsList.cs
	let i=i+1
  fi
done <achievements.txt

echo "}" >> AchievementsList.cs
