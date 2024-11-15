shader_type canvas_item;

uniform float flash_speed = 0;

void fragment() {
	// Called for every pixel the material is visible on.
	float current_flash = abs(cos(TIME * flash_speed));
	vec4 texture_color = texture(TEXTURE, UV);
	vec4 final_color = vec4(texture_color.r, texture_color.g, texture_color.b, current_flash * texture_color.a); 

	COLOR = final_color;
}
