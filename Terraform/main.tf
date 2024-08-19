# Define required providers
terraform {
  required_version = ">= 0.14.0"
  required_providers {
    openstack = {
      source = "terraform-provider-openstack/openstack"
    }
    local = {
      source = "hashicorp/local"
    }
  }
}

# Configure the OpenStack provider with authentication details
provider "openstack" {
  auth_url    = "https://cloud.4c.dhbw-mannheim.de:5000" # OpenStack authentication URL
  tenant_id = var.tenant_id       # OpenStack tenant name
  user_name   = var.user_name    # OpenStack user name
  password    = var.password     # OpenStack password
  domain_name = "default"
}

# Define the resource to create VMs in OpenStack
resource "openstack_compute_instance_v2" "my_openstack_instance" {
  name            = "${var.vm_name}" # VM name pattern
  image_name      = var.image_name     # Image to use for the VMs
  flavor_name     = var.flavor_name    # Flavor (size) of the VMs
  key_pair        = var.key_pair       # SSH key pair to use
  security_groups = [var.security_group] # Security group to apply
  network {
    name = var.network_name            # Network to attach the VMs to
  }
}

resource "openstack_compute_floatingip_v2" "my_ip" {
  pool = "DHBW"
}

resource "openstack_compute_floatingip_associate_v2" "my_ip" {
  floating_ip = openstack_compute_floatingip_v2.my_ip.address
  instance_id = openstack_compute_instance_v2.my_openstack_instance.id
}

resource "local_file" "hosts_cfg" {
  content = templatefile("${path.module}/Ansible/hosts.tpl",
    {
      vm_ips = [openstack_compute_floatingip_v2.my_ip.address]
    }
  )
  filename = ".${path.module}/hosts.cfg"
}
