// Include standard headers
#include <stdio.h>
#include <stdlib.h>

// Include GLEW
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>
GLFWwindow* window;

// Include GLM
#include <glm/glm.hpp>
using namespace glm;

#include <common/shader.hpp>
#include <glm/gtc/matrix_transform.hpp>

int main( void )
{
	// Initialise GLFW
	if( !glfwInit() )
	{
		fprintf( stderr, "Failed to initialize GLFW\n" );
		getchar();
		return -1;
	}

	glfwWindowHint(GLFW_SAMPLES, 4);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE); // To make MacOS happy; should not be needed
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	// Open a window and create its OpenGL context
	window = glfwCreateWindow(2560, 1440, "Two triangles", NULL, NULL);
	if( window == NULL ){
		fprintf( stderr, "Failed to open GLFW window. If you have an Intel GPU, they are not 3.3 compatible. Try the 2.1 version of the tutorials.\n" );
		getchar();
		glfwTerminate();
		return -1;
	}
	glfwMakeContextCurrent(window);

	// Initialize GLEW
	glewExperimental = true; // Needed for core profile
	if (glewInit() != GLEW_OK) {
		fprintf(stderr, "Failed to initialize GLEW\n");
		getchar();
		glfwTerminate();
		return -1;
	}

	// Ensure we can capture the escape key being pressed below
	glfwSetInputMode(window, GLFW_STICKY_KEYS, GL_TRUE);

	// Dark blue background
	glClearColor(0.0f, 0.0f, 0.05f, 0.0f);

	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	GLuint VertexArrayID;
	glGenVertexArrays(1, &VertexArrayID);
	glBindVertexArray(VertexArrayID);

	// Create and compile our GLSL program from the shaders
	GLuint programID[] = {
	LoadShaders("SimpleTransform.vertexshader", "SimpleFragmentShader.fragmentshader"),
	LoadShaders("SimpleTransform.vertexshader", "SimpleFragmentShader2.fragmentshader"),
};

	// Get a handle for our "MVP" uniform
	GLuint MatrixID[] = {
		glGetUniformLocation(programID[0], "MVP"),
		glGetUniformLocation(programID[1], "MVP"),

	};

	// Projection matrix : 90° Field of View, 16:9 ratio, display range : 0.1 unit <-> 100 units
	glm::mat4 Projection = glm::perspective(glm::radians(90.0f), 16.0f / 9.0f, 0.1f, 100.0f);

	
	// Model matrix : an identity matrix (model will be at the origin)
	glm::mat4 Model = glm::mat4(1.0f);


	static const GLfloat g_vertex_buffer_data[] = {
		-0.4f, -0.8f, 0.0f,
		 0.4f, -0.8f, 0.0f,
		 0.0f,  0.1f, 0.0f,
		-0.4f,  0.8f, 0.0f,
		 0.4f,  0.8f, 0.0f,
		 0.0f, -0.1f, 0.0f,
	};

	GLuint vertexbuffer;
	glGenBuffers(1, &vertexbuffer);
	glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
	glBufferData(GL_ARRAY_BUFFER, sizeof(g_vertex_buffer_data), g_vertex_buffer_data, GL_STATIC_DRAW);

	double angle = 0;

	do{

		// Clear the screen
		glClear( GL_COLOR_BUFFER_BIT );

		// Camera matrix
		glm::mat4 View = glm::lookAt(
			glm::vec3(cos(angle) / sqrt(2), cos(angle) / sqrt(2), -sin(angle)), // Camera rotating
			glm::vec3(0, 0, 0), // and looks at the origin
			glm::vec3(0, 1, 0)  // Head is up 
		);


		// Our ModelViewProjection : multiplication of our 3 matrices
		glm::mat4 MVP = Projection * View * Model; // Remember, matrix multiplication is the other way around


		// Use our shader
		int i = 0;
		while (i < 2) {
			glUseProgram(programID[i]);

			// 1rst attribute buffer : vertices
			glEnableVertexAttribArray(0);
			glUniformMatrix4fv(MatrixID[0], 1, GL_FALSE, &MVP[0][0]);
			glBindBuffer(GL_ARRAY_BUFFER, vertexbuffer);
			glVertexAttribPointer(
				0,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
				3,                  // size
				GL_FLOAT,           // type
				GL_FALSE,           // normalized?
				0,                  // stride
				(void*)0            // array buffer offset
			);

			// Draw the triangle !
			glDrawArrays(GL_TRIANGLES, 0 + i * 3, 3); // 3 indices starting at 0 -> 1 triangle
			glDisableVertexAttribArray(0);
			++i;
		}

		// Swap buffers
		glfwSwapBuffers(window);
		glfwPollEvents();

		angle += 0.01;

	} // Check if the ESC key was pressed or the window was closed
	while( glfwGetKey(window, GLFW_KEY_ESCAPE ) != GLFW_PRESS &&
		   glfwWindowShouldClose(window) == 0 );

	// Cleanup VBO
	glDeleteBuffers(1, &vertexbuffer);
	glDeleteVertexArrays(1, &VertexArrayID);

	glDeleteProgram(programID[0]);
	glDeleteProgram(programID[1]);

	// Close OpenGL window and terminate GLFW
	glfwTerminate();

	return 0;
}

