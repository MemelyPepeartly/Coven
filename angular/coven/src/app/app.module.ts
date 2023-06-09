import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { AuthorComponent } from './author/author.component';
import { WorldComponent } from './world/world.component';
import { SectionComponent } from './section/section.component';
import { SummaryComponent } from './summary/summary.component';
import { ArticleComponent } from './article/article.component';
import { ArticleListComponent } from './article-list/article-list.component';

import { BbcodePipe } from './pipes/bbcode.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AuthorComponent,
    WorldComponent,
    SectionComponent,
    SummaryComponent,
    ArticleComponent,
    ArticleListComponent,
    BbcodePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
