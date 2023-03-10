import { Component, Input } from '@angular/core';
import { ModalService } from 'src/app/services/modal.service';
import { UserService } from 'src/app/services/user.service';
import { TodoFormComponent } from '../todo-form/todo-form.component';
import { Todo } from '../todo.model';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.scss'],
})
export class TodoItemComponent {
  @Input() todo: Todo;

  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {}

  onEditClicked() {
    this.userService.setCurrentUserTodo(this.todo);
    this.modalService.setModalContent({
      component: TodoFormComponent,
      data: {
        isEdit: true,
      },
    });
    this.modalService.setModalIsVisible(true);
  }
}
