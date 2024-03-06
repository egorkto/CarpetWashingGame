mergeInto(LibraryManager.library, {
	ShowRewardedAd: function() {
		ShowRewarded();
	},
	ShowFullscreenAd: function() {
		ShowFullscreen();
	},
	
	LoadExtern: function() {
		LoadData();
	},

	SaveExtern: function(data) {
	    var str = UTF8ToString(data);
        var json = JSON.parse(str);
	    SaveData(json);
	},

	SetScore: function(score) {
		SetLeaderboardScore(score);
	},

	CheckString: function(str) {
		Check(UTF8ToString(str));
	}
})