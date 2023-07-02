<template>
  <a-modal :title="title" :width="900" :visible="visible" :confirmLoading="confirmLoading" @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="字典名称" prop="name" hasFeedback>
              <a-input placeholder="请输入字典名称" v-model="form.name" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="唯一键值" prop="code" hasFeedback>
              <a-input placeholder="请输入唯一键值" v-model="form.code" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="是否启用">
              <a-switch id="visible" checkedChildren="是" v-model="form.enable" unCheckedChildren="否" />
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>

    </a-spin>
  </a-modal>
</template>

<script>
export default {
  data() {
    return {
      gutter: 24,
      mdSize: 24,
      smSize: 24,
      title: '',
      formType: '',
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      visible: false,
      confirmLoading: false,
      form: {},
      rules: {
        name: [{ required: true, message: '请输入字典名称！' }],
        code: [{ required: true, message: '请输入唯一键值！' }]
      },
    }
  },
  methods: {
    // 初始化方法
    detail(record) {
      if (record) {
        this.form = record
        this.formType = 'edit'
        this.title = '编辑字典'
      } else {
        this.form = createModal()
        this.formType = 'add'
        this.title = '新增字典'
      }
      this.visible = true
    },
    close() {
      this.$emit('close')
      this.visible = false
    },
    handleOk() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.confirmLoading = true
          if (this.formType === 'edit') {
            this.$http.put('/sysDic', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
              } else {
                this.$message.warning(res.message)
              }
            }).finally(() => {
              this.confirmLoading = false
              this.visible = false
            })
          }
          else {
            this.$http.post('/sysDic', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
              } else {
                this.$message.warning(res.message)
              }
            }).finally(() => {
              this.confirmLoading = false
              this.visible = false
            })
          }
        }
      })
    },
    handleCancel() {
      this.close()
    }
  }
}
function createModal() {
  return {
    enable: true
  }
}
</script>
