# Unity WebGL Touch Bug
This repository describes an input reading error for Touch devices on the WebGL platform

[BugReproduce.webm](https://github.com/Dan0398/Unity-WebGL-Touch-Bug/assets/90954515/ded48fa3-7f82-4f6c-9089-157dd9d68e2c)

### Description
Current Unity3D project - sample of dragging a cube by screen to pointer position (mouse or touch).
HTML page of our app contains 2 parts:
- Top part - Unity3D Canvas with 90% of page height.
- Bottom part - clickable HTML element (button in our case) with 10% of page height.

Problems going when we try to click between parts on touch devices (at least on Windows all's OK). But this 2 buggy implementations works different.

1. With "Legacy Input System". After switch between parts reaction doesn't comes at first click (sometimes not in second and third). Uncomfortable, but workable.
2. With "New Input System" it's much interesting. After click at button there is spawns some zone (as i can describe) that overrides pointer positions.
<details>
<summary>Scheme</summary>

![image](/Media/Description.png "Usage of component inside editor")
</details>
When we creates button on top, diagonal doesn't changed direction and  all inputs blocked.

### How to try by yourself
In releases i created a prebuild version that you can open in browser (but you must host it with some web server).

Problems detected on:
- Pixel 3a, Android 12, Chrome and Firefox.
- Genymotion Android 13 x86, Default WebView app (capture video at link above)

### Workaround
I already [encountered Unity Technologies](https://forum.unity.com/threads/commandbuffer-setglobaltexture-works-weird.1538248/) support. And, as i think, they will consider it unimportant. And i create a solution. Just load "Workaround" scene and explore a solution.
