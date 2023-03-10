import { Injectable } from '@angular/core';
import { User } from '../user/user.model';
import { Subject } from 'rxjs';
import { getFormattedDate } from '../shared/utils/date';
import { Todo } from '../todo/todo.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  usersChanged = new Subject<User[]>();
  userSelected = new Subject<User>();
  private currentUser: User;
  private users: User[] = [
    {
      id: '1',
      fullName: 'Danny',
      todos: [
        {
          description: 'Learn Angular',
          dateCreated: getFormattedDate(new Date()),
          dateCompleted: '',
          complete: false,
        },
      ],
    },
    { id: '2', fullName: 'Simba', todos: [] },
  ];
  private currentUserTodo: Todo;

  getUsers() {
    return this.users.slice();
  }

  getCurrentUser() {
    return this.currentUser;
  }

  setCurrentUser(user: User) {
    this.currentUser = user;
    this.userSelected.next(this.currentUser);
  }

  getCurrentUserTodo() {
    return this.currentUserTodo;
  }

  setCurrentUserTodo(todo: Todo) {
    this.currentUserTodo = todo;
  }
}
