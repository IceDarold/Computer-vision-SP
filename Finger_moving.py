import bpy
import time
import math

for handler in bpy.app.handlers.frame_change_pre[:]:
    bpy.app.handlers.frame_change_pre.remove(handler)
def update_scene(scene):
    # Ваш код для изменения объектов на сцене
    # Например, перемещение куба
    cube = bpy.data.objects.get("Cube")
    with open("D:/data.txt", "r") as f:
        x, y, z = map(float, f.readline().split(","))
        print(x, y, z)
    if cube:
        cube.rotation_euler.x = math.sin(time.time())
        cube.rotation_euler.z = math.sin(time.time())
        cube.rotation_euler.y = math.sin(time.time())
        cube.location.x = x * 100
        cube.location.z = (1 - y) * 100
#        cube.location.y = z * 100
# Регистрация обработчика событий
bpy.app.handlers.frame_change_pre.append(update_scene)

# Включение анимации
bpy.context.scene.frame_start = 1
bpy.context.scene.frame_end = 300
bpy.context.scene.frame_current = 1
bpy.context.scene.render.fps = 24
bpy.context.scene.render.image_settings.file_format = 'AVI_JPEG'

# Включение анимации
bpy.ops.screen.animation_play()
#bpy.app.handlers.frame_change_pre.remove(update_scene)
#bpy.ops.screen.animation_cancel()
