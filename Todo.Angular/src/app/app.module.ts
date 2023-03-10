import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ListCardComponent } from './shared/list-card/list-card.component';
import { UserCardComponent } from './user/user-card/user-card.component';
import { TodoCardComponent } from './todo/todo-card/todo-card.component';
import { UserItemComponent } from './user/user-item/user-item.component';
import { TodoItemComponent } from './todo/todo-item/todo-item.component';
import { ModalComponent } from './shared/modal/modal.component';
import { ModalOverlayComponent } from './shared/modal/modal-overlay/modal-overlay.component';
import { ModalCardComponent } from './shared/modal/modal-card/modal-card.component';
import { UserFormComponent } from './user/user-form/user-form.component';
import { TextInputComponent } from './shared/text-input/text-input.component';
import { TodoFormComponent } from './todo/todo-form/todo-form.component';
import { ModalContentDirective } from './shared/modal/modal-content.directive';
import { CheckInputComponent } from './shared/check-input/check-input.component';

@NgModule({
  declarations: [
    AppComponent,
    ListCardComponent,
    UserCardComponent,
    TodoCardComponent,
    UserItemComponent,
    TodoItemComponent,
    ModalComponent,
    ModalOverlayComponent,
    ModalCardComponent,
    UserFormComponent,
    TextInputComponent,
    TodoFormComponent,
    ModalContentDirective,
    CheckInputComponent,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [BrowserModule, FormsModule, ReactiveFormsModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
