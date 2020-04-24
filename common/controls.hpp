#ifndef CONTROLS_HPP
#define CONTROLS_HPP

void computeMatricesFromInputs();
void computeMatricesFromInputsSize(GLFWwindow* window);
glm::mat4 getViewMatrix();
glm::mat4 getProjectionMatrix();

#endif