import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Input from '@material-ui/core/Input';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';

const styles = theme => ({
  input: {
    margin: theme.spacing.unit,
  },
});

export default ({
  addTodo,
  todos
}) =>  {
  const [state, setState] = useState({
    taskName: ''
  });
  return (
    <div>
      <div>
        <Input
          value = {state.taskName}
          inputProps={{
            'aria-label': 'Description',
          }}
          onChange = {e => setState({
            taskName : e.target.value
          })}
        />
        <Button 
          variant="contained"
          color="primary"
          onClick={() => {
            addTodo(state.taskName);
            setState({
              taskName: ''
            });
          }}>
          Add
        </Button>
      </div>
      <div>
        <List component="nav">
          {todos.map((element, index) => 
            <ListItem 
              key={index} >
              <ListItemText 
                primary= {element.name} />
            </ListItem>
          )}
        </List>
      </div>
    </div>
  )
}