import React from 'react'

const Upload = () => {
  return (
    <div className="modal" id="uploadBox" tabIndex="-1" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Upload your data</h5>
            <button type="button" className="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div className="modal-body text-center">
            <p>Accepted File Types: *.csv, *.txt</p>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-primary">Upload</button>
            <button type="button" className="btn btn-secondary" data-dismiss="modal">Cancel</button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Upload
