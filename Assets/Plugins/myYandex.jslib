mergeInto(LibraryManager.library, {
  RewardedVideoExtern: function(){
      myGameInstance.SendMessage("Managers/LevelManager","SkipLevel")
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          myGameInstance.SendMessage("Managers/LevelManager","SkipLevel")
          console.log('Rewarded!');
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },
  FullScreenExtern: function(){
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: () =>{
          console.log('Video ad closed.');
        },
        onError: () => {
          console.log('Уккщк.');
        }
    }
})
  },
  

});