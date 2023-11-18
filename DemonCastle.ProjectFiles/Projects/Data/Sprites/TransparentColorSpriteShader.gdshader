shader_type canvas_item;

uniform vec4 transparent_color: source_color;
uniform float threshold;

void fragment() {
	vec4 col = texture(TEXTURE, UV);
	if (length(abs(col - transparent_color)) < threshold) {
		COLOR = vec4(0, 0, 0, 0);
	} else {
		COLOR = col;
	}
}