from keras.models import load_model
from keras.preprocessing.image import ImageDataGenerator

img_size = 50
batch_size = 16

model = load_model('models/bc_4xCNN_4xFCL_25epochs.h5')
model.compile(
	loss='categorical_crossentropy',
	optimizer='rmsprop',
    metrics=['accuracy']
    )

datagen = ImageDataGenerator()

test_generator = datagen.flow_from_directory(
        '../data/coins/test',
        target_size=(img_size, img_size),
        batch_size=batch_size,
        class_mode='categorical',
        shuffle=False,
        color_mode='rgb'
        )

test_samples = len(test_generator.filenames)

score = model.evaluate_generator(
	test_generator, 
	test_samples // batch_size, 
	workers=8, 
	erbose=1
	)

print("Loss: ", score[0], "\nAccuracy: ", score[1])
