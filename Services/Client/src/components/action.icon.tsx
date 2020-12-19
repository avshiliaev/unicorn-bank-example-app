import React from 'react';

const ActionIcon = ({ text, action }) => {
  return (
    <div onClick={() => action()}>
            <span>
                <div style={{ marginRight: 8 }}/>
              {text}
            </span>
    </div>
  );
};

export default ActionIcon;
