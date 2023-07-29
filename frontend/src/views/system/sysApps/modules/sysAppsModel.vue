<template>
  <a-modal :title="title" :width="800" :visible="visible" :confirmLoading="confirmLoading" @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="spinningLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
            <a-col :md="mdSize" :sm="smSize">
              <a-form-model-item label="应用名称" prop="name" hasFeedback>
                 <a-input v-model="form.name" placeholder="" />
              </a-form-model-item>
            </a-col>
        </a-row>
        <a-row :gutter="gutter">
            <a-col :md="mdSize" :sm="smSize">
              <a-form-model-item label="应用编码" prop="code" hasFeedback>
                 <a-input v-model="form.code" placeholder="" />
              </a-form-model-item>
            </a-col>
        </a-row>
        <a-row :gutter="gutter">
            <a-col :md="mdSize" :sm="smSize">
              <a-form-model-item label="排序" prop="order" hasFeedback>
                 <a-input v-model="form.order" placeholder="" />
              </a-form-model-item>
            </a-col>
        </a-row>
        <a-row :gutter="gutter">
            <a-col :md="mdSize" :sm="smSize">
              <a-form-model-item label="启用" prop="enable">
                 <a-switch checkedChildren="启用" v-model="form.enable" unCheckedChildren="禁用" />
               </a-form-model-item>
            </a-col>
        </a-row>
        <a-row :gutter="gutter">
            <a-col :md="mdSize" :sm="smSize">
              <a-form-model-item label="描述" prop="describe" hasFeedback>
                 <a-input v-model="form.describe" placeholder="" />
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
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      gutter: 48,
      mdSize: 24,
      smSize: 24,
      formType: 'add',
      title: '添加数据',
      spinningLoading: false,
      visible: false,
      confirmLoading: false,
      rules: {
      },
      form: {}
    }
  },
  methods: {
    // 打开页面初始化
    detail(value) {
      this.visible = true
      this.confirmLoading = false
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      if (value) {
        this.form = JSON.parse(JSON.stringify(value))
        this.formType = 'edit'
        this.title = '编辑数据'
      }
      else {
        this.title = '新建数据'
        this.formType = 'add'
        this.form = createForm()
      }
    },
    async handleOk() {
      this.$refs.form.validate(async valid => {
        if (valid) {
          this.confirmLoading = true    
          // 修改数据
          if (this.formType === 'edit') {
            await this.$http.put('/sysApps', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(err => {
              this.confirmLoading = false
            })
          } else {
            // 保存数据
            // 先上传文件
            await this.$http.post('/sysApps', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(err => {
              this.confirmLoading = false
            })
          }
        }
      })
    },
    handleCancel() {
      this.close()
    },
    close() {
      this.$emit('close')
      this.visible = false
    }
  }
}
function createForm() {
  return {
   
  }
}
</script>