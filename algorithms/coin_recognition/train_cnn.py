from keras.models import Sequential
from keras.layers import Activation, Dropout, Flatten, Dense
from keras.preprocessing.image import ImageDataGenerator
from keras.layers import Conv2D, MaxPooling2D

img_size = 50
batch_size = 16
epochs = 25
color_mode = 'rgb'
class_mode = 'categorical'

train_dir = '../data/coins/train'
validation_dir = '../data/coins/validation'

datagen = ImageDataGenerator(rescale=1./255)

train_generator = datagen.flow_from_directory(
        train_dir,
        color_mode=color_mode,
        target_size=(img_size, img_size),
        batch_size=batch_size,
        class_mode=class_mode
        )
		


validation_generator = datagen.flow_from_directory(
        validation_dir,
        color_mode=color_mode,
        target_size=(img_size, img_size),
        batch_size=batch_size,
        class_mode=class_mode
        )

train_samples = len(train_generator)
validation_samples = len(validation_generator)

model = Sequential()

model.add(Conv2D(32, 3, input_shape=(img_size, img_size, 3), padding='same'))
model.add(Activation('relu'))
model.add(MaxPooling2D(pool_size=2, strides=2))

model.add(Conv2D(64, 3, padding='same'))
model.add(Activation('relu'))
model.add(MaxPooling2D(pool_size=2, strides=2))

model.add(Conv2D(128, 3, padding='same'))
model.add(Activation('relu'))
model.add(MaxPooling2D(pool_size=2, strides=2))

model.add(Flatten())
model.add(Dense(64))
model.add(Activation('relu'))
model.add(Dropout(0.1))

model.add(Dense(64))
model.add(Activation('relu'))
model.add(Dropout(0.1))

model.add(Dense(64))
model.add(Activation('relu'))
model.add(Dropout(0.1))

model.add(Dense(64))
model.add(Activation('relu'))
model.add(Dropout(0.1))

model.add(Dense(5))
model.add(Activation('softmax'))

# model.summary()
# print(train_generator.class_indices)

model.compile(
    loss='categorical_crossentropy',
    optimizer='rmsprop',
    metrics=['accuracy']
    )

model.fit_generator(
        train_generator,
        steps_per_epoch=train_samples,
        epochs=epochs,
        validation_data=validation_generator,
        validation_steps=validation_samples
        )

# model.save('models/bc_3xCNN_4xFCL_25epochs.h5')