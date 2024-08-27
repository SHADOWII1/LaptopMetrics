<style>
  .center {
  display: block;
  margin-left: auto;
  margin-right: auto;
}
</style>

# Cloud Native Application: LaptopMetrics

**Name**: Hamza Harti | **Matrikelnummer**: 2431662 | **Modul**: W3M20035 | **Professoren**: Prof. Dr.-Ing. habil. Dennis Pfisterer & Prof. Dr. Christoph Sturm

# Table of Contents

1. [Part I: Why Cloud Native Application?](#part-i-why-cloud-native-app)
   1. [Introduction](#introduction)
   2. [The Advantages and Disadvantages of Cloud Native Approach](#the-advantages-and-disadvantages-of-cloud-native-approach)
      1. [The Advantages of Cloud Native Approach](#the-advantages-of-cloud-native-approach)
      2. [The Disadvantages of Cloud Native Approach](#the-disadvantages-of-cloud-native-approach)
   3. [Alternatives to Cloud Native Approach](#alternatives-to-cloud-native-approach)
      1. [Traditional Monolithic Applications](#traditional-monolithic-applications)
      2. [Hybrid Cloud Models](#hybrid-cloud-models)
      3. [Serverless Computing](#serverless-computing)
   4. [Data Security and The GDPR](#data-security-and-the-gdpr)

2. [Part II: The Development of Cloud Native Application](#part-ii-the-development-of-cloud-native-application)

##  1. <a name='part-i-why-cloud-native-app'></a>Part I: Why Cloud Native Application?

###  1.1. <a name='introduction'></a> Introduction

Over the last couple of years, the term "Cloud Native" has become very popular among software developers and in circles dealing with IT infrastructure. As organizations have undergone progressive digital transformation, cloud-native applications have become an essential constituent of modern IT strategies. The term "cloud-native" refers to the design, development, and deployment of applications specifically intended to exploit the scalability, resilience, and flexibility provided by the cloud. Overall, cloud-native applications are normally based on microservices architectures, managed by container orchestration systems like Kubernetes, and deployed into environments that support methodologies for continuous integration and continuous delivery. The move toward cloud-native development is a serious one and offers many benefits, specifically for applications that are rate-limited by extreme scalability, dealing with vast amounts of data, or high availability. However, like any technology solution, cloud-native approaches bring their own set of problems and drawbacks. Moreover, other strategies, including traditional monolithic architectures or hybrid cloud models, have their strengths but also limitations, which must be carefully considered in choosing the best approach for an application.

Cloud-native architectures have not only revolutionized application development, deployment, and management—offering flexibility, scalability, and resilience in ways previously unthought of—but also helped applications like *LaptopMetrics*, designed to monitor the performance of the host system, immensely. When moved to a cloud infrastructure, *LaptopMetrics* enables its users to leverage real-time monitoring, data analysis, and reporting capabilities with the very cloud-native framework in use while ensuring high availability and scalability.

###  1.2. <a name='the-advantages-and-disadvantages-of-cloud-native-approach'></a> The Advantages and Disadvantages of Cloud Native Approach

The Cloud Native approach offers various advantages that benefit the application *LaptopMetrics* after a successful implementation. However significant disadvantages present themselves, which require a great deal of considerations.

#### 1.2.1 <a name='the-advantages-of-cloud-native-approach'></a> The Advantages of Cloud Native Approach

The following table summarizes some of the advantages the Cloud Native Application inherits after Implementation:

| Feature                    | Description                                                                                     |
|----------------------------|-------------------------------------------------------------------------------------------------|
| **Scalability**            | Cloud-native architecture allows LaptopMetrics to scale resources dynamically.                  |
| **Resilience**             | Ensures continuous availability even in the event of component failures.                        |
| **Flexibility and Agility**| Enables rapid deployment and updates, allowing the app to adapt quickly.                       |
| **Cost Efficiency**        | Optimizes resource usage and costs by scaling according to demand.                              |
| **Improved Developer Productivity** | Enhances development speed and collaboration through microservices and CI/CD.           |

1. **Scalability**  
   Cloud-native architecture allows LaptopMetrics to scale resources dynamically based on demand. This is crucial for handling fluctuating loads. During high demand, additional microservice instances can be deployed to manage the load, ensuring optimal performance. Conversely, during low activity periods, resources can be scaled down to optimize costs without sacrificing performance.

2. **Resilience**  
   LaptopMetrics benefits from high availability and fault tolerance due to its microservices architecture and cloud platform deployment (Kubernetes). If a service fails, such as the data collection service, it can be automatically restarted or redirected to a healthy instance. This minimizes downtime and ensures that critical monitoring functions continue to operate without interruption, which is essential for monitoring.

3. **Flexibility and Agility**  
   The cloud-native architecture provides LaptopMetrics with the ability to adapt quickly to changing requirements. Updates, such as new performance metrics or features, can be developed, tested, and deployed rapidly. Containers and CI/CD pipelines facilitate this agility, allowing the development team to implement changes with minimal disruption and keep the application aligned with user needs.

4. **Cost Efficiency**  
   LaptopMetrics benefits from cost efficiency by utilizing only the necessary resources at any given time. This is particularly advantageous for handling variable workloads. For instance, during large-scale deployments, the application can scale up to manage the increased load and then scale down afterward, reducing costs and ensuring resource efficiency.

5. **Improved Developer Productivity**  
   Enhanced productivity is achieved through microservices, containers, and CI/CD practices. Development teams can work on different application components simultaneously, reducing bottlenecks and accelerating feature delivery. Containerization ensures consistent code execution across development, testing, and production environments, minimizing environment-related issues and allowing developers to focus on delivering value.

#### 1.2.2 <a name='the-disadvantages-of-cloud-native-approach'></a> The Disadvantages of Cloud Native Approach

When opting for a cloud native approach, the developer should take into account various considerations. Some of the main considerations are featured in the following table:


| Consideration          | Description                                                                                             |
|------------------------|---------------------------------------------------------------------------------------------------------|
| **Increased Complexity** | Cloud-native architecture introduces complexity in development and management.                        |
| **Operational Overhead** | Requires sophisticated monitoring and management tools, increasing operational overhead.                |
| **Vendor Lock-In**      | Dependency on specific cloud providers could limit flexibility and options for future migrations.       |
| **Security Concerns**   | Increases the attack surface and necessitates robust security practices to protect against vulnerabilities. |

1. **Increased Complexity**  
   One of the primary disadvantages of deploying LaptopMetrics as a cloud-native application is the increased complexity. The microservices architecture, while offering scalability and flexibility, also introduces a level of complexity in terms of development, deployment, and management. Each service within LaptopMetrics must communicate effectively with others, requiring careful orchestration and management. This complexity can lead to challenges in debugging issues or making changes, especially as the application grows in size and scope.

2. **Operational Overhead**  
   Operational overhead is another concern. Managing a cloud-native application like LaptopMetrics requires sophisticated monitoring, logging, and tracing solutions to ensure that all microservices are functioning correctly and efficiently. This increases the operational burden on the team, who must also manage the underlying infrastructure, handle updates, and ensure that the application remains secure. The need for specialized tools and expertise can also add to the overall cost and complexity.

3. **Vendor Lock-In**  
   Vendor lock-in is a potential risk for LaptopMetrics, particularly if the application relies heavily on specific cloud provider services. For example, if LaptopMetrics uses AWS-specific services for data storage or processing, moving to a different cloud provider could be challenging and costly. This reliance on a single vendor could limit the application's flexibility and make it difficult to take advantage of more competitive offerings from other providers.

4. **Security Concerns**  
   Security concerns are heightened in a cloud-native environment due to the increased attack surface. LaptopMetrics, like any cloud-native application, must ensure that data is secure at all times—whether in transit, at rest, or during processing. The distributed nature of microservices and the use of third-party services require robust security practices, which can be resource-intensive and complex to implement.

###  1.3. <a name='alternatives-to-cloud-native-approach'></a> Alternatives to Cloud Native Approach

While there are many benefits to cloud-native development, that does not make it the only approach to application development. Depending on the organizational needs, technical specifications, or preceding infrastructure, other methodologies could be more appropriate. Three of these alternatives are traditional monolithic applications, hybrid cloud models, and serverless computing. Each of the aforementioned approaches has a unique set of benefits and shortcomings.

####  1.3.1 <a name='traditional-monolithic-applications'></a> Traditional Monolithic Applications

A traditional monolithic architecture is an alternative to cloud-native development. In a monolithic application, all components are tightly coupled and run as a single unit. While this approach simplifies development and deployment, it comes with limitations in terms of scalability, flexibility, and resilience.

Monolithic applications can be easier to develop and manage for small, simple use cases. However, as the application grows, it becomes more challenging to maintain, scale, and update. The lack of modularity means that even small changes require redeploying the entire application, leading to longer development cycles and increased risk of downtime.

####  1.3.2 <a name='hybrid-cloud-models'></a> Hybrid Cloud Models

Hybrid cloud models offer a middle ground between traditional on-premises applications and cloud-native solutions. In a hybrid cloud, organizations deploy some components of the application in the cloud while keeping others on-premises. This approach allows organizations to leverage the benefits of cloud computing while retaining control over critical infrastructure and data.

Hybrid cloud models are particularly useful for organizations with legacy systems that cannot be easily migrated to the cloud. However, managing a hybrid environment can be complex and requires careful coordination between on-premises and cloud-based components.

####  1.3.2 <a name='serverless-computing'></a> Serverless Computing

Serverless computing is another alternative to cloud-native development. In a serverless model, the cloud provider automatically manages the infrastructure, scaling resources up and down as needed. Developers focus solely on writing code, while the cloud provider handles deployment, scaling, and maintenance.

Serverless computing offers many of the benefits of cloud-native development, such as scalability and cost-efficiency, without the need to manage infrastructure. However, it also comes with limitations, such as potential latency issues, cold start times, and limited control over the environment.

###  1.4. <a name='data-security-and-the-gdpr'></a> Data Security and The GDPR

Security is of utmost importance, particularly regarding data security in the context of Cloud Native Applications, which raises a significant question: How can the security of the handled data be ensured, and what impact do legal regulations such as the General Data Protection Regulation (GDPR) have on the application? 

| **Security Aspect**                  | **Description**                                                                          |
|--------------------------------------|------------------------------------------------------------------------------------------|
| **Encryption**                       | Ensures that data is protected both in transit and at rest.                              |
| **Identity and Access Management (IAM)** | Controls access to sensitive data through fine-grained permissions.                       |
| **Secure Software Development Lifecycle (SDLC)** | Integrates security throughout the development process.                                    |
| **Container Security**               | Protects the integrity of containerized applications and their dependencies.              |
| **Network Security**                 | Safeguards communication between microservices and external systems.                     |

1. **Encryption**
   Data security is a great concern for LaptopMetrics, especially given the sensitive nature of performance data. Encryption is to be used both at rest and in transit. Performance metrics collected from the host are to be encrypted before being transmitted to the cloud, ensuring that data cannot be intercepted or tampered with. Similarly, data stored in cloud databases is encrypted at rest, protecting it from unauthorized access even if the storage medium is compromised.

2. **Controlled Access**
   Access to the virtual machine (VM) provided by Openstack is restricted to authorized personnel only. An SSH key is required to establish an encrypted connection to the VM, ensuring protection against unauthorized access.

3. **Secure Software Development Lifecycle (SDLC)**
   Security is to be integrated throughout the SDLC to identify and mitigate vulnerabilities early in the development process. This should includes regular code reviews, automated security testing, and continuous monitoring for potential security threats. Embedding security practices throughout the development lifecycle helps reduce the risk of security breaches and ensures that the application remains secure as it evolves.

4. **Container Security**
   For LaptopMetrics, deployed as a cloud-native application, container security is essential. Containers are to be built from trusted images, regularly updated, and scanned for vulnerabilities. Runtime security measures are also to be in place to detect and prevent malicious activities, such as unauthorized access to containers or abnormal behavior that could indicate a security breach.

5. **Network Security**
   Securing the network infrastructure supporting LaptopMetrics is critical. Measures such as network segmentation, firewalls, and security groups control traffic flow between microservices and external systems. Communication between microservices is to be encrypted, and secure protocols are to be used to ensure that data is not exposed to unauthorized parties. These measures help protect data and ensure compliance with necessary security standards.

Most of the security Aspects are not implemented in the development of the cloud native application, however they should be considered, especially of the application is to be moved into production. Ownership rights to the data should also be clarified, ideally through Service Level Agreements (SLAs) and Service Level Objectives (SLOs). Such agreements should specify whether the data is considered the property of the user or the provider. 

Data ownership rights can vary depending on the laws and regulations of different countries. Therefore, it is important to ensure that agreements with the cloud provider comply with the legal requirements of the respective country, especially when personal data is collected.  

##  2. <a name='part-ii-the-development-of-cloud-native-application'></a>Part II: The development of Cloud Native Application


<img src="img/Architecture.svg" alt="architecture" width="100%" class="center"/>
