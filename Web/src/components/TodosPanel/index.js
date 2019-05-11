import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Input from '@material-ui/core/Input';

const styles = theme => ({
  input: {
    margin: theme.spacing.unit,
  },
});

export default ({
  addTodo
}) =>  {
  const [state, setState] = useState({
    taskName: ''
  });
  return (
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
  )
}