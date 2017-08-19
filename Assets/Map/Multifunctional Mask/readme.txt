This package includes a set of shaders with very wide variability. All effects 
can be applied to post-effects, graphic interface elements and 3D objects. 
In all cases, the rendered texture taken before drawing a particular object or 
the camera image is taken as the basis. Among the presented effects are a lot 
of different variants of Blur, Bloom and Glow. In addition, you can adjust the 
gamma, the contrast of the image, get a negative, make a linear color correction, 
highlight the transition between the tones, apply chromatic noise, imitate the 
drawing with a pencil or chalk and much, much more. A set of 3D shaders also 
allows you to make a simple simulation of glass, both transparent and matte.


The principle of operation of this set of shaders is to apply for each pixel 
the sum of a set of pixels of pixels of its neighborhood with certain coefficients.
The Offset (Size) property defines the size of the neighborhood from which the set 
of pixels for the summation is taken.
Properties QualityX, QualityY determine the number of pixels taken from this 
neighborhood along the X and Y axes in Cartesian shader variants and along the 
circular and radial axis in polar variants. 
They directly affect the quality of the resulting image (in the demo scene these are 
Quality properties and X_Y koef where Quality is the highest value from QualityX and 
Quality, and X_Y koef is their ratio)
The Mask texture (Mask) property defines a map of the neighborhood pixel coefficients.
Properties Iteration min (Iter min) and Iteration max (Iter max) to which the 
coefficients are respectively at 0 and 1 mask values. For each color channel, 
the summation occurs separately.
Min and Max set the values ??in which the resulting pixel is converted at 0 and 1.
The Offset mask map property (Intensive mask) determines the dependence of the 
Offset (Size) property relative to the screen coordinates.
In full shader variants, sampling of pixels simultaneously on 2 axes is taken, which 
allows using almost any mask, but it strongly affects the performance. 
In simplified versions, sampling and summation occur in 2 passes, separately for each 
axis, which in most cases speeds up execution, especially for large QualityX and 
QualityY values. 
On some platforms (for example, WebGL) it is not possible to use the custom properties 
QualityX and QualityY for them there is a set of shaders marked with const (when using 
post-effects with given shaders, the simplified approach does not yield a performance gain).
The main operating principle of this set of shaders is that for every pixel there is the 
certain sum of surrounding pixels with the relevant coefficients is applied.
The option Offset(Size) presents the the size of the area, from which the sum of pixels 
of the current location is taken.
The options QualityX, QualityY define the number of pixels taken from that area according 
to the axis X, Y