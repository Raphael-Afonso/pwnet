# Perfect World Emulator - Refactoring Angelica

## About the Project

This project is an emulator for the game **Perfect World**, inspired by and based on the work done in the [Angelica Emulator](https://github.com/marceloalencar/angelica-emulator). The idea was born from a long-standing passion for the game and programming, which began back in 2009 during my school years when I started exploring software development. Around the same time, I discovered **Ragezone**, where I learned the fundamentals of reverse engineering.

### Motivation

Recently, after studying **.NET 8** and **event-driven architectures**, I was looking for a project to solidify the knowledge I had gained. Revisiting the Angelica Emulator seemed like the perfect opportunity to modernize and enhance the project, making it more efficient and robust.

### Current Progress

The project is still in its early stages. At the moment, only part of the **handshake during login** is functional. There’s a lot of work ahead to turn this into a complete and functional emulator. Nonetheless, the focus from the outset has been on creating a solid, modular foundation for future development.

---

## Technologies Used

- **.NET 8**: The primary development framework.
- **Event-Driven Architecture**: Implemented using a custom event dispatcher and handler system I developed, without relying on external messaging systems.
- **SQL**: Used as the backend for data persistence.
- **EF Core**: For database interaction and management.
- **IOptions**: For configuration management with `appsettings`.
- **BCrypt**: For securely hashing user passwords stored in the database.
- **Clean Architecture**: Ensuring separation of concerns and maintainability.

---

## Roadmap

Planned features and functionalities include:

1. **Complete Handshake at Login** (partially implemented). 
2. Implementation of basic character systems.
3. Creation of world systems (movement, NPCs, etc.).
4. Efficient communication with the Perfect World client.
5. Support for instances and quests.
6. Improvements in event management and performance optimizations.

---

## Licensing

This project is provided "as-is," without support or warranty of any kind. Feel free to use it as you see fit. For legal purposes, it is distributed under the **MIT License**.

---

## Acknowledgments

This project reflects years of learning and passion. It would not have been possible without the community of developers and enthusiasts who shared their knowledge over time. Special thanks to **Ragezone** and the contributors to the **Angelica Emulator**, whose work serves as an inspiration for this effort.

---

**Current Status:** Under Development 🚧  
**Author:** Raphael Fernandes