import React, { useState } from 'react';

export default ({
  addTodo
}) =>  {
  const [state, setState] = useState('');
  return (
    <div>
      <input value={state} onChange={e => setState(e.target.value)}/>
      <button
        onClick={() => {
          addTodo(state);
          setState('');
        }}>
        Add
      </button>
    </div>
  )
}