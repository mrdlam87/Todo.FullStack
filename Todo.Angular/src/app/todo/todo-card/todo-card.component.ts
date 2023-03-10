import { Component, OnInit } from '@angular/core';
import { ModalService } from 'src/app/services/modal.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/user/user.model';
import { TodoFormComponent } from '../todo-form/todo-form.component';
import { Todo } from '../todo.model';

@Component({
  selector: 'app-todo-card',
  templateUrl: './todo-card.component.html',
  styleUrls: ['./todo-card.component.scss'],
})
export class TodoCardComponent implements OnInit {
  todos: Todo[] = [];

  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.userService.userSelected.subscribe((user: User) => {
      this.todos = user.todos;
    });
  }

  addTodo() {
    if (this.userService.getCurrentUser()) {
      this.modalService.setModalContent({
        component: TodoFormComponent,
        data: {
          isEdit: false,
        },
      });
      this.modalService.setModalIsVisible(true);
    } else {
      alert('Please select a user first');
    }
  }
}
