# Define variables for the OpenStack configuration
variable "tenant_id" {}
variable "user_name" {}
variable "password" {}

# Number of VMs to create
variable "vm_count" {
  default = 1
}

# Base name for the VMs
variable "vm_name" {
  default = "WebApp_MetricsLaptop"
}

# Image to use for the VMs
variable "image_name" {
  default = "Ubuntu Server 22.04 64bit"
}

# Flavor (size) of the VMs
variable "flavor_name" {
  default = "m1.xlarge"
}

# SSH key pair to use for the VMs
variable "key_pair" {
  default ="Hamza_Key"
}

# Security group to apply to the VMs
variable "security_group" {
  default = "default"
}

# Network to attach the VMs to
variable "network_name" {
  default = "cloud23"
}
