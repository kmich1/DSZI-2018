import sys
import numpy as np
from keras.models import load_model
from keras.preprocessing import image

img_size = 50
imgs_list = []

model = load_model('models/bc_3xCNN_4xFCL_25epochs.h5')
model.compile(
	loss='categorical_crossentropy',
    optimizer='rmsprop',
    metrics=['accuracy']
    )
			  
for img_path in sys.argv[1:]:
	img = image.load_img(img_path, target_size=(img_size, img_size))
	img = image.img_to_array(img)
	img = np.expand_dims(img, axis=0)
	imgs_list.append(img)

predictions = model.predict_classes(np.vstack(imgs_list))
sys.stdout.write(str(predictions)[1:-1])