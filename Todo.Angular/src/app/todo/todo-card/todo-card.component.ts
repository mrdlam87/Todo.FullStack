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
    this.userService.userSelected.subscribe(() => {
      this.userService
        .getUserTodos()
        .subscribe((todos) => (this.todos = todos));
    });
    this.userService.todosChanged.subscribe(() =>
      this.userService.getUserTodos().subscribe((todos) => (this.todos = todos))
    );
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
