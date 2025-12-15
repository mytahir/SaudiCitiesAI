**SaudiCitiesAI** is a modern, developer-first API that provides **structured data about Saudi Arabian cities**, enriched with **AI-generated insights** powered by OpenStreetMap and AI models.

It is designed to help developers, startups, researchers, and enterprises **build smarter applications around Saudi cities**, tourism, Vision 2030, and location-based intelligence.

---

## 🌟 Why SaudiCitiesAI?

Saudi Arabia is undergoing one of the largest transformations in the world — yet **city-level, developer-friendly data is fragmented**.

SaudiCitiesAI solves this by combining:

* 🌍 **OpenStreetMap (OSM)** for real geospatial data
* 🧠 **AI Insights** for human-readable city intelligence
* 🏗️ **Clean Architecture** for long-term scalability
* ⚡ **Simple REST API** for fast integration

---

## 🚀 Core Features

### 🏙️ City Data API

* List Saudi cities
* Search cities by name
* Get city details (coordinates, region, metadata)
* Powered by **OpenStreetMap Overpass API**

### 🧭 Attractions API

* Retrieve attractions by city
* Search attractions by name
* Categorized (tourism, culture, landmarks, etc.)

### 🤖 AI City Insights

Generate AI-powered insights for any Saudi city:

* Tourism highlights
* Cultural overview
* Vision 2030 relevance
* Business & investment potential
* City summaries for apps & dashboards

> AI insights work **even if the city is not stored in the database** — fetched live from OSM.

### 🔐 Lightweight Authentication (Optional)

* Email-based user registration
* Secure API key (hashed storage)
* Usage tracking & analytics-ready
* **No passwords required**

### 📊 User Activity Tracking

* City search history
* AI query history
* Foundation for dashboards & analytics

---

## 🧠 Example AI Insight

```json
{
  "content": "Al Khobar is a vibrant coastal city in Saudi Arabia's Eastern Province, known for its Corniche, business hubs, and proximity to Bahrain. It plays a growing role in tourism and Vision 2030 initiatives..."
}
```

---

## 🛠️ Tech Stack

| Layer         | Technology                   |
| ------------- | ---------------------------- |
| Backend       | ASP.NET Core (.NET 9)        |
| Architecture  | Layered Architecture         |
| Database      | MySQL + EF Core (Pomelo)     |
| Maps Data     | OpenStreetMap (Overpass API) |
| AI            | LongCat AI API               |
| ORM           | Entity Framework Core        |
| Documentation | Swagger / OpenAPI            |

---

## 🏗️ Architecture Overview

```
API Layer
 ├── Controllers
 ├── Request / Response DTOs
 │
Application Layer
 ├── Services (Use Cases)
 ├── Interfaces
 │
Domain Layer
 ├── Entities
 ├── Value Objects
 ├── Business Rules
 │
Infrastructure Layer
 ├── EF Core (MySQL)
 ├── OpenStreetMap Integration
 ├── Repositories
 │
AI Layer
 ├── LongCat Client
 ├── Prompt Templates
 ├── AI Services
```

✔ Clean separation
✔ No circular dependencies
✔ Easily testable
✔ Future-proof

---

## 🔍 API Capabilities

### Cities

* `GET /api/cities`
* `GET /api/cities/{id}`
* `GET /api/cities/search?q=riyadh`

### Attractions

* `GET /api/attractions/city/{cityId}`
* `GET /api/attractions/search?q=park`

### AI Insights

* `POST /api/ai/insights/city/{cityId}`
* `POST /api/ai/insights/city/search`

---

## 🌍 Who Is This API For?

### 🧳 Hajj & Umrah Platforms

* City exploration for pilgrims
* Cultural & historical insights
* Nearby attractions & landmarks

### 🏢 Business & Investment Apps

* City intelligence dashboards
* Market research tools
* Location-based feasibility studies

### 🗺️ Tourism & Travel Apps

* Smart city guides
* AI-generated travel summaries
* Dynamic recommendations

### 📊 Government & Vision 2030 Tools

* Urban analytics
* City transformation tracking
* Open data enrichment

### 🧠 AI & Research Projects

* City-level AI summarization
* Geospatial + NLP experiments
* Knowledge graph foundations

---

## 🔮 Roadmap

* [ ] Cache OSM cities into database
* [ ] City ranking & scoring
* [ ] Vision 2030 metrics per city
* [ ] Rate limiting per API key
* [ ] Multi-language AI insights
* [ ] Admin analytics dashboard

---

## 📄 Open Data & Licensing

* City data powered by **OpenStreetMap**
* Data licensed under **ODbL**
* AI content generated dynamically

---

## 🤝 Contributing

Contributions are welcome!

* Architecture discussions
* Performance improvements
* New AI prompts
* Data enrichment ideas

---

## 🧠 Vision

> **SaudiCitiesAI aims to become the go-to developer platform for Saudi city intelligence — combining open data, AI, and clean engineering.**
