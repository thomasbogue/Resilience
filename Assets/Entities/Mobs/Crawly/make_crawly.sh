#!/bin/bash

infiles=$( ls crawly-[123456789].png)

convert +append $infiles -resize 128x128 crawly.png

#montage crawly-[123456789]-small.png -tile 9x1 -geometry +0+0 crawly.png
