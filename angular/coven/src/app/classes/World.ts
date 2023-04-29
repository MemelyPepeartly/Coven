class World {
    id: string;
    name: string;
    locale: string;
    description: string;
    description_parsed: string;
    display_css: string;
    theme: number;
    tags: string;
    slug: string;
    url: string;
    author: Author;
  
    constructor() {
      this.id = '';
      this.name = '';
      this.locale = '';
      this.description = '';
      this.description_parsed = '';
      this.display_css = '';
      this.theme = 0;
      this.tags = '';
      this.slug = '';
      this.url = '';
      this.author = new Author();
    }
  }
  