# IES_PROJECT_PR162_2020

Projekat (Broj 5) iz predmeta Integracija Elektroenergetskih Sistema na smeru Primenjeno Softverski Inzinjerstvo 2022.

# UML graph
![image](https://github.com/Dualxyz/IES_PROJECT_PR162_2020/assets/17052851/73d342f9-2051-40e0-b4a2-3cdabdcfd620)

# Order of project execution
- NetworkModelServiceSelfHost - Hosts an instance of the server which is going to be used to apply delta to the classes that are sent from ModelLabsApp.
- ModelLabsApp - WPF Application that takes the XML (xmldump.xml) that was generated with the help of CIMET. By pressing Apply Delta we initialize the communication with the server.
- NMSTestClient - Contains a CLI for obtaining values based on specific parameters.

# Concrete Classes
- Connectivity Node Container
- ConnectivityNode
- DC Line Segment
- Series Compensator
- Terminal

